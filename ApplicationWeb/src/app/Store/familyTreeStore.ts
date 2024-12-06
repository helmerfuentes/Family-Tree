// https://www.stefanos-lignos.dev/posts/ngrx-signals-store#useful-links---examples
import { inject } from "@angular/core";
import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { SnackbarSeverityEnum } from "../Core/Enums/snackbar-severity.enum";
import { AppMessageService } from "../Core/Services/app-message.service";
import { CityService } from "../Core/Services/City/City.service";
import { CountryService } from "../Core/Services/Country/country.service";
import { DepartmentService } from "../Core/Services/Department/Department.service";
import { PersonService } from "../Core/Services/Person/Person.service";
import { RelationshipService } from "../Core/Services/Relationship/Relationship.service";
import { VillageService } from "../Core/Services/village/village.service";
import { familyTreeInitialState, FamilyTreeState } from "./models/familyTree";

export const FamilytreeStore = signalStore(
  { providedIn: 'root' },
  withState<FamilyTreeState>(familyTreeInitialState),
  withComputed((store) => ({
    // Add computed here
  })),
  withMethods(
    (
      store,
      departmentService = inject(DepartmentService),
      countryService = inject(CountryService),
      cityService = inject(CityService),
      villageService = inject(VillageService),
      personService = inject(PersonService),
      messageService = inject(AppMessageService),
      relationshipService = inject(RelationshipService)
    ) => ({
      showSnackbarMessage(message: string, severity: SnackbarSeverityEnum) {
        messageService.showSnackbarMessage(message, severity);
      },
      showLoadingModal(value: boolean, text: string = "Cargando") {
        patchState(store, { loading: { showLoading: value, text: text } });
      },
      // LOCATION
      async getDepartmentsAsync() {
        var response = await countryService.GetAllCountryWithDepartmentsAsync()
        if (response) {
          patchState(store, { countryList: response.data as any })
        }
      },

      async GetCitiesByDepartmentIdAsync(departmentId: number) {
        var response = await cityService.GetCitiesByDepartmentIdAsync(departmentId)
        return response.data;
      },

      async GetVillagesByCityIdAsync(cityId: number) {
        var response = await villageService.GetVillagesByCityIdAsync(cityId)
        return response.data;
      },

      // PERSON METHODS
      async GetPersonByFilterAsync(filter: string, value: string) {
        var response = await personService.GetPersonByFilterAsync(filter, value)
        return response.data;
      },

      async GetFullInformationByIdAsync(id: number) {
        var response = await personService.GetFullInformationByIdAsync(id)
        return response.data;
      },

      async RegisterPersonAsync(data: any) {
        var response = await personService.RegisterAsync(data)
        return response.data;
      },

      //RELATIONSHIPS
      async RegisterChildAsync(data: any) {
        var response = await relationshipService.RegisterChildAsync(data)
        this.showSnackbarMessage('Hijo registrado correctamente', SnackbarSeverityEnum.success);
        return response.data;
      },

      async GetRelationshipByPersonIdAsync(personId: number, getParents: boolean) {
        var response = await relationshipService.GetRelationshipByPersonIdAsync(personId, getParents)
        return response.data;
      },
    })
  )
);