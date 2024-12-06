import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notFound',
  templateUrl: './notFound.component.html',
  styleUrls: ['./notFound.component.css'],
  standalone: true
})
export class NotFoundComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  
  irHome(){
    this.router.navigate(['/']);
  }
}
