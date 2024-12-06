import { AfterViewInit, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import cytoscape from 'cytoscape';
import { ConfirmationService } from 'primeng/api';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { DialogModule } from 'primeng/dialog';
import { InformationPersonComponent } from '../../components/informationPerson/informationPerson.component';
import { SnackbarSeverityEnum } from '../../Core/Enums/snackbar-severity.enum';
import { FamilytreeStore } from '../../Store/familyTreeStore';
import { SearchPeopleComponent } from '../relationship/Components/searchPeople/searchPeople.component';
@Component({
  selector: 'app-designer',
  templateUrl: './designer.component.html',
  styleUrls: ['./designer.component.scss'],
  standalone: true,
  imports: [
    SearchPeopleComponent,
    ConfirmPopupModule,
    InformationPersonComponent,
    DialogModule
  ],
  providers: [
    ConfirmationService
  ]
})
export default class DesignerComponent implements OnInit, AfterViewInit {
  @ViewChild('graphContainer', { static: true }) graphContainer!: ElementRef;
  @ViewChild(SearchPeopleComponent) searchPeopleP!: SearchPeopleComponent;
  private cy!: cytoscape.Core;
  public flywheelStore = inject(FamilytreeStore);
  public label: string = '';
  public visible: boolean = false;
  nodoKeySelected: number = 0;
  constructor(private confirmationService: ConfirmationService) { }

  ngOnInit(): void {
    // Inicializar Cytoscape
    this.cy = cytoscape({
      container: this.graphContainer.nativeElement, // Contenedor del grafo
      style: [
        {
          selector: 'node',
          style: {
            'height': 80,
            'width': 80,
            'background-color': 'white',
            'background-fit': 'cover',
            'border-color': '#6F42C1',
            'border-width': 3,
            'border-opacity': 0.5,
            'background-image': 'data(image)',
            'label': 'data(label)',
          },
        },
        {
          selector: 'edge',
          style: {
            'width': 6,
            'target-arrow-shape': 'triangle',
            'line-color': '#6F42C1',
            'target-arrow-color': '#ffaaaa',
            'curve-style': 'bezier',
          },
        }
        
      ],
      layout: {
        name: 'breadthfirst', // Puedes cambiar el layout: grid, circle, breadthfirst, etc.
        directed: true,
        padding: 10
      },
    });

    this.cy.on('click', 'node', (event) => {
      this.showNodeSelected(event);
    });

    this.cy.on('cxttap', 'node', (event) => {
      this.confirm1(event);
    });
  }

  showNodeSelected(event: any) {
    const nodeData = event.target.data();
    this.searchPeopleP.setData(nodeData.data);
    this.visible = true;
    this.nodoKeySelected = nodeData.id;
  }

  confirm1(event: any) {
    const nodeData = event.target.data();
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `¿Qué elementos deseas expandir de ${nodeData.label}?`,
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Padres',
      rejectLabel: 'Hijos',
      accept: async () => {
        await this.GetRelationshipsAsync(nodeData.id, true, event.position);
      },
      reject: async () => {
        await this.GetRelationshipsAsync(nodeData.id, false, event.position);
      },
      
    });
  }

  async GetRelationshipsAsync(id:number, getParent:boolean, positionNode: { x: number; y: number }) {
    this.flywheelStore.showLoadingModal(true, 'Buscando relaciones');
    var response = await this.flywheelStore.GetRelationshipByPersonIdAsync(id, getParent);
    this.flywheelStore.showLoadingModal(false);
    
    if (response.length == 0) {
      this.flywheelStore.showSnackbarMessage('No se encontraron relaciones.', SnackbarSeverityEnum.info);
      return;
    }
    
    if (positionNode) {
      if (getParent) {
        positionNode.y = positionNode.y - 100;
      }else{
        positionNode.y = positionNode.y + 100;
      }    
    }

    response.forEach((element: any) => {
      const image = this.getImage(element);
      const node = { id: element.key, label: element.fullNames, data: element, image: image };
      const edges = getParent 
        ? [{ source: id.toString(), target: element.key }]
        : [{ source: element.key, target: id.toString() }];

      this.addNodeAndEdge(this.cy, node, edges, positionNode);
    });
  }

  addNodeAndEdge(
    cy: cytoscape.Core, 
    newNode: { id: string; label: string,data:any, image: string }, 
    newEdges: { source: string; target: string }[],
    positionNode: { x: number; y: number },
  
  ) {
    // Agregar el nuevo nodo
    cy.add({
      group: 'nodes',
      data: newNode,
      position: { x:positionNode.x+10, y: positionNode.y },
    });

    newEdges.forEach(edge => {
      const sourceId = edge.source.toString();
      const targetId = edge.target.toString();
      // Comprobar si ya existe una arista de source -> target o target -> source
      const existingEdge = cy.edges().filter((e: cytoscape.EdgeSingular) => {
        return (
          (e.source().id() === sourceId && e.target().id() === targetId) ||
          (e.source().id() === targetId && e.target().id() === sourceId)
        );
      });
  
      // Si no existe la relación, agregar la nueva arista
      if (existingEdge.length === 0) {
        cy.add({
          group: 'edges',
          data: { source: sourceId, target: targetId },
        });
      }
    });
  }

  ngAfterViewInit() {
    const person = this.flywheelStore.person();
    this.label = this.getLabelGender(person.gender);
    
    if (this.searchPeopleP) {
      this.searchPeopleP.setData(this.flywheelStore.person());
    }
    const image = this.getImage(person);
    const node:any={x: (Math.random() * 900 - 100), y: 60}
    const nodeRoot = { id: person.key, label: person.fullNames, data: person, image: image };
    this.addNodeAndEdge(this.cy, nodeRoot, [],node);
  }

  getImage(person:any){
    const image = person.gender == 1 
    ? './assets/icons/hombre.png'
    : './assets/icons/mujer.png';
    return image;
  }

  getLabelGender(gender: number) {
    if (gender == 0) {
      return 'Indefinido';

    } else if (gender == 1) {
      return 'F';
    }
    return 'M';
  }

}
