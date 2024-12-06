import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';
import { FamilytreeStore } from '../../Store/familyTreeStore';

@Component({
  standalone: true,
  selector: 'app-nacionality',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DropdownModule,
    AutoCompleteModule
  ],
  templateUrl: './nacionality.component.html',
  styleUrls: ['./nacionality.component.css']
})
export class NacionalityComponent implements OnInit {
  @Output() formValidChange = new EventEmitter<boolean>();
  
  public form!: FormGroup;
  public departments: any[] = [];
  public cities: any[] = [];
  public villages: any[] = [];
  filteredCountries: any[] = [];
  filteredDepartments: any[] = [];
  filteredCities: any[] = [];
  filteredVillages: any[] = [];

  public flywheelStore = inject(FamilytreeStore);

  constructor(private fb: FormBuilder,private cdRef: ChangeDetectorRef) { }

  async ngOnInit() {
    this.form = this.fb.group({
      country: ['', Validators.required],
      department: ['', Validators.required],
      city: ['', Validators.required],
      village: ['']
    });

    this.form.statusChanges.subscribe(() => {
      this.formValidChange.emit(this.form.valid);
    });
    
    await this.flywheelStore.getDepartmentsAsync();
  }

  filterCountry(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.flywheelStore.countryList().length; i++) {
      let country = this.flywheelStore.countryList()[i];
      if (country.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(country);
      }
    }

    this.filteredCountries = filtered;
  }

  filterDepartment(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.departments.length; i++) {
      let department = this.departments[i];
      if (department.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(department);
      }
    }

    this.filteredDepartments = filtered;
  }

  filterCity(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.cities.length; i++) {
      let city = this.cities[i];
      if (city.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(city);
      }
    }

    this.filteredCities = filtered;
  }

  filterVillage(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.villages.length; i++) {
      let village = this.villages[i];
      if (village.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(village);
      }
    }

    this.filteredVillages = filtered;
  }

  onCountrySelect($event: AutoCompleteSelectEvent) {
    this.departments = $event.value.departments;
    this.filteredDepartments =[];
    this.filteredCities = [];
    this.filteredVillages = [];
    this.cities = [];
  }

  async onDepartmentSelect($event: AutoCompleteSelectEvent) {
    var response = await this.flywheelStore.GetCitiesByDepartmentIdAsync($event.value.key);
    if (response) {
      this.cities = response;
    }
    this.filteredCities = [];
    this.filteredVillages = [];
    this.cdRef.detectChanges();
  }

  async onCitySelect($event: AutoCompleteSelectEvent) {
    var response = await this.flywheelStore.GetVillagesByCityIdAsync($event.value.key);
    if (response) {
      this.villages = response;
    }
    this.filteredVillages = [];
    this.cdRef.detectChanges();
  }
}
