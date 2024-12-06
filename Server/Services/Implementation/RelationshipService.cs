using AutoMapper;
using Microsoft.Data.Sqlite;
using Server.Extensions;
using Server.Helper;
using Server.Models;
using Server.Resources;
using Server.UnitOfWork;
using Server.ViewModels;
using Server.ViewModels.Request;
using Server.ViewModels.Response.Relationship;

namespace Server.Services.Implementation
{
    public class RelationshipService : IRelationshipService
    {
        private readonly string _connectionString;
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateRelationshipAsync(RelationshipRequest relationshipRequest)
        {
            ValidationHelper.ValidateObject(relationshipRequest, nameof(relationshipRequest));
            ValidationHelper.ValidateIntValue(relationshipRequest.ChildId, nameof(relationshipRequest.ChildId));
            ValidationHelper.ValidateIntValue(relationshipRequest.ParentId, nameof(relationshipRequest.ParentId));
            ValidationHelper.ValidateIntValue(relationshipRequest.MotherId, nameof(relationshipRequest.MotherId));

            //TODO: para primera aplicar la relacion donde estem ambos padres, la posterior puede ser un nuclke familiar con solo la madre o el padre
            if (relationshipRequest.ParentId == default || relationshipRequest.MotherId == default)
                throw new ApplicationException("Debe seleccionar los dales de los padres");

            var father = await _unitOfWork.Persons.GetByIdAsync(relationshipRequest.ParentId);
            if (relationshipRequest.ParentId != default && father == null)
                throw new ApplicationException("El padre especificado no existe.");

            var mother = await _unitOfWork.Persons.GetByIdAsync(relationshipRequest.MotherId);
            if (relationshipRequest.MotherId != default && mother == null)
                throw new ApplicationException("La madre especificada no existe.");

            var child = await _unitOfWork.Persons.GetByIdAsync(relationshipRequest.ChildId);
            if (child == null)
                throw new ApplicationException("El hijo especificado no existe.");

            var containsParentDistinc = (child.FatherId != default && child.FatherId != relationshipRequest.ParentId) || (child.MotherId != default && child.MotherId != relationshipRequest.MotherId);
            if (containsParentDistinc)
            {
                throw new ApplicationException("Hijo ya tiene un padre o madre asignado, verifique");
            }

            //TIENEN RELACION DE AMBOS PADRES, MEJORAR CUANDO SOLO TENGA PADRE O MADRE
            var tableName = $"father_{father.Id}_mother_{mother.Id}";
            await EnsureTableExistsAsync(tableName);
            await InsertRelationshipAsync(tableName, child, relationshipRequest.IsBiologic);

            child.Father = father;
            child.Mother = mother;
            _unitOfWork.Persons.Update(child);

            var relation = await _unitOfWork.Relationships.FindAsync(r => r.TableName == tableName);
            if (!relation.HasValues())
            {
                Relationship relationship = new Relationship
                {
                    Father = father,
                    Mother = mother,
                    TableName = tableName
                };
                _unitOfWork.Relationships.Add(relationship);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<RelationshipResponse>> GetRelationshipByPersonIdAsync(int id, bool parents)
        {
            var relationships = new List<RelationshipResponse>();

            var person = await _unitOfWork.Persons.GetByIdWithIncludesAsync(id, m => m.Mother, f => f.Father);
            if (person == null)
                throw new ApplicationException("La persona especificada no existe.");

            if (parents)
            {
                if (person.Mother != default && person.Father != default)
                {
                    var relationMother = new RelationshipResponse
                    {
                        FullNames = person.Mother.FullNames,
                        Key = person.Mother.Id,
                        Gender = person.Mother.Gender,
                        Description = person.Mother.Description
                    };

                    var relationFather = new RelationshipResponse
                    {
                        FullNames = person.Father.FullNames,
                        Key = person.Father.Id,
                        Gender = person.Father.Gender,
                        Description = person.Father.Description
                    };

                    relationships.Add(relationMother);
                    relationships.Add(relationFather);
                }

            }
            else
            {
                var relationsOfPerson = await _unitOfWork.Relationships.FindAsync(r => r.FatherId == id || r.MotherId == id);

                if (relationsOfPerson.HasValues())
                {
                    var tasks = relationsOfPerson
                       .Select(async relation =>
                       {
                           // Llamar al método asincrónico y devolver los datos de la tabla
                           return await GetDynamicTableDataAsync(relation.TableName);
                       }).ToList();
                    var results = await Task.WhenAll(tasks);
                    List<DynamicTableModel> dynamicData = results.SelectMany(data => data).ToList();
                    IEnumerable<Person> peoples = await _unitOfWork.Persons.FindAsync(p => dynamicData.Select(d => d.ChildId).Contains(p.Id));

                    relationships = peoples?
                        .Select(data => new RelationshipResponse
                        {
                            FullNames = data.FullNames,
                            Key = data.Id,
                            Description = data.Description,
                            Gender = data.Gender
                        }).ToList();
                }
            }

            return relationships;
        }

        private async Task<List<DynamicTableModel>> GetDynamicTableDataAsync(string tableName)
        {
            var data = new List<DynamicTableModel>();

            // Formatear la consulta usando el archivo de recursos
            string query = string.Format(SqlStatement.GetInfoDynamicTable, tableName);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var relationship = new DynamicTableModel
                            {
                                Id = reader.GetInt32(0),
                                ChildId = reader.GetInt32(1),
                                BiologicalChild = reader.GetInt32(2),
                                FullNames = reader.GetString(3)
                            };

                            data.Add(relationship);
                        }
                    }
                }
            }

            return data;
        }

        private async Task EnsureTableExistsAsync(string tableName)
        {
            // Formatear el script para crear la tabla, usando el nombre de la tabla como parámetro
            string formattedCreateScript = string.Format(SqlStatement.CreateDynamicTable, tableName);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(formattedCreateScript, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }



        private async Task InsertRelationshipAsync(string tableName, Person child, int isBiologicalChild)
        {
            // Formatear el script de inserción
            string formattedInsertScript = string.Format(SqlStatement.InsertIntoDynamicTable, tableName);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(formattedInsertScript, connection))
                {
                    // Agregar parámetros para la inserción
                    command.Parameters.AddWithValue("@ChildId", child.Id);
                    command.Parameters.AddWithValue("@BiologicalChild", isBiologicalChild);
                    command.Parameters.AddWithValue("@ChildFullNames", child.FullNames);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
