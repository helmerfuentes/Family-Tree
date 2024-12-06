import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../../Core/Services/Department/Department.service';
import { InformationPersonComponent } from '../../components/informationPerson/informationPerson.component';
import { LoadingComponent } from '../../components/loading/loading.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports:[
  CommonModule,
  LoadingComponent,
  InformationPersonComponent
  ],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export default class MainComponent implements OnInit {
  constructor(private departmentService: DepartmentService) { }
  
  async ngOnInit() {
    // const res = await this.departmentService.getDepartmentsAsync();
    // debugger;
  }

}
