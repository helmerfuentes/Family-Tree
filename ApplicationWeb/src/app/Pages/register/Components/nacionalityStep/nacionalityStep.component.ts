import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, inject, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';
import { FamilytreeStore } from '../../../../Store/familyTreeStore';
import { NacionalityComponent } from '../../../../components/nacionality/nacionality.component';

@Component({
  standalone: true,
  selector: 'app-nacionalityStep',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DropdownModule,
    AutoCompleteModule,
    NacionalityComponent
  ],
  templateUrl: './nacionalityStep.component.html',
  styleUrls: ['./nacionalityStep.component.css']
})
export class NacionalityStepComponent implements OnInit {
  @ViewChild(NacionalityComponent) nacionalityComponent!: NacionalityComponent;
  @Output() formValidChange = new EventEmitter<boolean>();

  public flywheelStore = inject(FamilytreeStore);
  public formValid: boolean = false;
  constructor(private fb: FormBuilder, private cdRef: ChangeDetectorRef) { }

  async ngOnInit() {
  }

  formValidChangeHandler(value: boolean) {
    this.formValid = value;
    this.formValidChange.emit(value);
  }

}
