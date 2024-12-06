import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { NacionalityComponent } from '../../../../components/nacionality/nacionality.component';

@Component({
  selector: 'app-informationDeath',
  standalone: true,
  imports:[
    CommonModule,
    ReactiveFormsModule,
    NacionalityComponent,
    CalendarModule
  ],
  templateUrl: './informationDeath.component.html',
  styleUrls: ['./informationDeath.component.css']
})
export class InformationDeathComponent implements OnInit {
   @ViewChild(NacionalityComponent) nacionalityComponent!: NacionalityComponent;

  @Output() formValidChange = new EventEmitter<boolean>();
  public form!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.form = this.fb.group({
      dateOfDeath: ['',Validators.required],
      placeOfDeath: ['',Validators.required],
      causeOfDeath: ['',Validators.required],
      city: ['',Validators.required],
      village: ['']
    });

    this.form.statusChanges.subscribe(() => {
      this.formValidChange.emit(this.form.valid);
    });
  }

  formValidChangeHandler(value: boolean) {
    if (value) {
      this.form.get('city')!.setValue(this.nacionalityComponent.form.get('city')!.value);
      this.form.get('village')!.setValue(this.nacionalityComponent.form.get('village')!.value);
    }else{
      this.form.get('city')!.setValue('');
      this.form.get('village')!.setValue('');
    }
  }
}
