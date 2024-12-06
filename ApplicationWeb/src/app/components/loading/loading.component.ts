import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FamilytreeStore } from '../../Store/familyTreeStore';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css'],
  standalone: true,
  imports: [
  CommonModule
  ]
})
export class LoadingComponent implements OnInit {
  protected readonly familyTreeStore = inject(FamilytreeStore);
  loading = this.familyTreeStore.loading;
  
  constructor() { }

  ngOnInit() {
  }

}
