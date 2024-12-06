import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import { CardModule } from 'primeng/card';
import { FamilytreeStore } from '../../Store/familyTreeStore';
@Component({
  selector: 'app-informationPerson',
  templateUrl: './informationPerson.component.html',
  styleUrls: ['./informationPerson.component.scss'],
  standalone: true,
  imports: [
    CardModule,
    CommonModule
  ]
})
export class InformationPersonComponent implements OnInit {
  @Input() personId!: number;
  
  private flywheelStore = inject(FamilytreeStore);
  data:any;
  constructor() { }

  async ngOnInit() {
    var data= await this.flywheelStore.GetFullInformationByIdAsync(this.personId);
    this.data=data;
  }

}
