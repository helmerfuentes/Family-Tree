import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-node-card',
  templateUrl: './node-card.component.html',
  styleUrls: ['./node-card.component.css'],
  standalone: true,
})
export class NodeCardComponent implements OnInit {
  @Input() label: string = ''; // El label del nodo
  constructor() { }

  ngOnInit() {
  }

}
