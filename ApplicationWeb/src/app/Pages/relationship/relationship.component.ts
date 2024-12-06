import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { patchState } from '@ngrx/signals';
import { ConfirmationService } from 'primeng/api';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { TooltipModule } from 'primeng/tooltip';
import { InformationPersonComponent } from '../../components/informationPerson/informationPerson.component';
import { SnackbarSeverityEnum } from '../../Core/Enums/snackbar-severity.enum';
import { FamilytreeStore } from '../../Store/familyTreeStore';
import RegisterComponent from '../register/register.component';
import { SearchPeopleComponent } from './Components/searchPeople/searchPeople.component';

const REGISTER_PERSON_BUTTON_CLICK = 1;
const DESCRIPTION_BUTTON_CLICK = 2;
const SEARCH_PERSON_BUTTON_CLICK = 3;
@Component({
  selector: 'app-relationship',
  templateUrl: './relationship.component.html',
  styleUrls: ['./relationship.component.scss'],
  standalone: true,
  imports: [
    AutoCompleteModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    TableModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    SearchPeopleComponent,
    RegisterComponent,
    TooltipModule,
    ConfirmDialogModule,
    CommonModule,
    InformationPersonComponent
  ],
  providers: [
    ConfirmationService
  ]
})
export default class RelationshipComponent implements OnInit {
  @ViewChild('searchPeopleP') searchPeopleP!: SearchPeopleComponent;
  @ViewChild('searchPeopleM') searchPeopleM!: SearchPeopleComponent;

  public flywheelStore = inject(FamilytreeStore);
  visible: boolean = false;
  suggestionsFilter!: any[];
  filterValue!: string;
  filterType!: string;
  filters: any[] = [
    {
      label: 'Nombre Completo',
      value: 'fullnames'
    },
    {
      label: 'Primer Apellido',
      value: 'firtsurname'
    },
    {
      label: 'Segundo Apellido',
      value: 'lastsurname'
    },
    {
      label: 'Identificacion',
      value: 'identification'
    }
  ];
  peoples!: any[];
  mother: any
  father: any
  openButtonDialogClick:number = 0;
  descriontionFull: string = '';
  personClicked:any;

  searchFilter(event: AutoCompleteCompleteEvent) {
    this.suggestionsFilter = [...this.filters];
  }

  constructor(private confirmationService: ConfirmationService, private router: Router) { }

  ngOnInit() {
  }

  selectedPerson(person: any, isParent: boolean) {
    if (isParent) {
      if (person.gender != 1) {
        this.flywheelStore.showSnackbarMessage('El padre debe ser de genero masculino', SnackbarSeverityEnum.info);
        return;
      }
      this.searchPeopleP.setData(person);
      this.father = person;
    } else {
      if (person.gender != 2) {
        this.flywheelStore.showSnackbarMessage('La madre debe ser de genero femenino', SnackbarSeverityEnum.info);
        return;
      }
      this.mother = person;
      this.searchPeopleM.setData(person);
    }
  }

  async selectedAsChild(person: any, event: Event) {
    if (!this.mother || !this.father) {
      this.flywheelStore.showSnackbarMessage('Debe seleccionar ambos padres', SnackbarSeverityEnum.info);
      return;
    }
    const data: any = {
      parentId: this.father.key,
      motherId: this.mother.key,
      childId: person.key
    }
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Seguro de agregar a ${person.names} como hijo de ${this.father.names} y ${this.mother.names}`,
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      accept: async () => {
        await this.flywheelStore.RegisterChildAsync(data);
      }
    });
  }
  showInformation(peolpe: any) {
    this.openButtonDialogClick = SEARCH_PERSON_BUTTON_CLICK;
    this.visible = true;
    this.personClicked = peolpe;
  }

  async searchPersonAsync() {
    if (!this.filterValue && !this.filterType) {
      this.flywheelStore.showSnackbarMessage('Debe escribir valor a buscar y selecionar el tipo de filtro', SnackbarSeverityEnum.error);
      return;
    }
    this.flywheelStore.showLoadingModal(true, 'Buscando persona');
    var peoples = await this.flywheelStore.GetPersonByFilterAsync(this.filterType, this.filterValue);
    this.peoples = peoples;
    if (peoples.length == 0) {
      this.flywheelStore.showSnackbarMessage('No se encontraron personas', SnackbarSeverityEnum.info);
    }
    this.flywheelStore.showLoadingModal(false);
  }

  goToDesigner(people: any) {
    patchState(this.flywheelStore, {  
      person: people
    });
    this.router.navigate(['/designer']);
  }

  openDialogRegisterPerson() {
    this.visible = true;
    this.openButtonDialogClick = REGISTER_PERSON_BUTTON_CLICK;
  }

  toggleDescription(description: string) {
    this.openButtonDialogClick = DESCRIPTION_BUTTON_CLICK;
    this.visible = true;
    this.descriontionFull = description;
  }

  getLabelGender(gender: number) {
    if (gender == 0) {
      return 'Indefinido';

    } else if (gender == 1) {
      return 'Masculino';
    }
    return 'Femenino';
  }

}
