import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, inject, OnInit, Output, ViewChild } from '@angular/core';

import { ReactiveFormsModule } from '@angular/forms';
import { MenuItem } from 'primeng/api';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { StepsModule } from 'primeng/steps';
import { SnackbarSeverityEnum } from '../../Core/Enums/snackbar-severity.enum';
import { PersonRequest } from '../../Core/Models/Request/PersonRequest';
import { FamilytreeStore } from '../../Store/familyTreeStore';
import { BasicDataComponent, } from './Components/basicData/basicData.component';
import { InformationComponent } from './Components/information/information.component';
import { InformationDeathComponent } from './Components/informationDeath/informationDeath.component';
import { NacionalityStepComponent } from './Components/nacionalityStep/nacionalityStep.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    StepsModule,
    InformationDeathComponent,
    NacionalityStepComponent,
    BasicDataComponent,
    InformationComponent
  ],
  providers: [
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],

})
export default class RegisterComponent implements OnInit {
  @ViewChild(BasicDataComponent) private basicDataComponent!: BasicDataComponent;
  @ViewChild(NacionalityStepComponent) private nacionalityStepComponent!: NacionalityStepComponent;
  @ViewChild(InformationDeathComponent) private informationDeathComponent!: InformationDeathComponent;
  @Output() savedSucess = new EventEmitter<boolean>();
  public flywheelStore = inject(FamilytreeStore);

  itemsComplete: MenuItem[] = [];
  items: MenuItem[] = [];
  activeIndex: number = 0;
  disableButton: boolean = false;
  isDeath: boolean = true;

  constructor(private changeDetector: ChangeDetectorRef) { }
  ngOnInit(): void {
    this.itemsComplete = [
      { label: 'Información', },
      { label: 'Datos basicos', visible: false },
      { label: 'Nacionalidad', visible: false },
      { label: 'Datos muerte', visible: false },
    ];

    this.items = this.itemsComplete;
  }

  prevStep() {
    if (this.activeIndex > 0) {
      this.activeIndex--;
    }
    this.disabledNextButton();
  }

  nextStep() {
    this.activeIndex++;
    this.disabledNextButton();
  }

  disabledNextButton(value: boolean = false) {
    if (this.activeIndex === 0) {
      value = false;
    }
    else if (this.activeIndex === 1 && this.basicDataComponent.isvalidForm()) {
      value = false;

    } else if (this.activeIndex === 2 && this.nacionalityStepComponent.formValid) {
      value = false;
    } else {
      value = true;
    }

    this.disableButton = value;
  }

  filterItemList(isDeath: boolean) {
    this.isDeath = isDeath;
    this.items = !isDeath
      ? this.itemsComplete.slice(0, -1)
      : [...this.itemsComplete];
    this.changeDetector.detectChanges();
  }

  async SaveRegisterAsync() {
    let formsComplete = false;
    if (this.isDeath) {
      formsComplete = this.informationDeathComponent.form.valid;
    } else {
      formsComplete = this.nacionalityStepComponent.formValid;
    }

    if (!formsComplete) {
      this.flywheelStore.showSnackbarMessage('Datos incompletos', SnackbarSeverityEnum.error);
      return;
    }
    // TODO: GUARDAR LA DATA EN EL STORE Y CONTRUIR EL OBEJCTO EN EL METHOD
    const basicData = this.basicDataComponent.form.value;
    const nacionalityData = this.nacionalityStepComponent.nacionalityComponent.form.value;
    const informationDeathData = this.informationDeathComponent.form.value;
    var personRequest: PersonRequest = {
      description: basicData.description,
      identification: basicData.identification,
      firstSurname: basicData.firstSurname,
      lastSurname: basicData.secondSurname,
      names: basicData.fullName,
      gender: +basicData.gender,
      cityId: nacionalityData.city,
      villageId: +nacionalityData.village,
      dateOfBirth: basicData.yearOfBirth,
      isDeceased: basicData.deceased,
      death: {
        dateOfDeath: basicData.deceased?informationDeathData.dateOfDeath: new Date(),
        causeOfDeath: informationDeathData.causeOfDeath,
        locationOfDeath: informationDeathData.placeOfDeath,
        cityId: +informationDeathData.city,
        villageId: +informationDeathData.village
      }

    }
    this.flywheelStore.showLoadingModal(true, 'Guardando persona');
    await this.flywheelStore.RegisterPersonAsync(personRequest);
    this.savedSucess.emit(true);
    this.flywheelStore.showSnackbarMessage('Persona registrada correctamente', SnackbarSeverityEnum.success);
    this.flywheelStore.showLoadingModal(false);
  }

  get showButtonSave() {
    return this.activeIndex === this.items.length - 1;
  }
}
