import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FamilytreeStore } from '../../../../Store/familyTreeStore';

@Component({
  selector: 'app-basic-data',
  standalone: true,
  imports:[
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    ButtonModule,
    FieldsetModule,
    CalendarModule,
    InputTextModule,
    DropdownModule,
    InputTextareaModule 
  ],
  templateUrl: './basicData.component.html',
  styleUrls: ['./basicData.component.css']
})
export class BasicDataComponent implements OnInit {
  @Output() formValidChange = new EventEmitter<boolean>();
  @Output() deceasedChange = new EventEmitter<boolean>();
  
  public form!: FormGroup;
  public flywheelStore = inject(FamilytreeStore);
  date: Date | undefined;


  constructor(private fb: FormBuilder) { }

  async ngOnInit() {
    this.form = this.fb.group({
      fullName: ['', Validators.required],
      firstSurname: ['', Validators.required],
      secondSurname: [''],
      identification: [''],
      gender: ['', Validators.required],
      yearOfBirth: ['', Validators.required],
      deceased: [true],
      description: ['',Validators.required],
    });

    this.form.statusChanges.subscribe(() => {
      this.formValidChange.emit(this.form.valid);
    });

    this.form.get('deceased')!.valueChanges.subscribe((value: boolean) => {
      this.deceasedChange.emit(value as boolean); // Emitir el nuevo valor
    });
  }

  isDeceased() {
    return this.form.get('deceased')?.value === true;
  }

  public isvalidForm(): boolean {
    return this.form.valid;
  }

}
