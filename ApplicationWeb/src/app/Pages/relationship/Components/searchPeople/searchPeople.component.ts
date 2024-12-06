import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { ButtonModule } from 'primeng/button';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-searchPeople',
  templateUrl: './searchPeople.component.html',
  styleUrls: ['./searchPeople.component.scss'],
  standalone: true,
  imports: [
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    AvatarModule,
    AvatarGroupModule,
    ButtonModule
  ]
})
export class SearchPeopleComponent implements OnInit {

  @Input() label!: string;
  protected data: any;
  constructor(private cd: ChangeDetectorRef ) { }

  ngOnInit() {
  }

  public setData(register: any) {
    this.data = register;
    this.cd.detectChanges();
  }

  public getICon() {
    if (this.label == 'P') {
      return 'pi pi-arrow-left'
    } else {
      return 'pi pi-arrow-right'
    }
  }

  public getSeverety() {
    if (this.label == 'M') {
      return 'info'
    } else {
      return 'success'
    }
  }

  public getColor() {
    if (this.label == 'M') {
      return '#0ea5e9'
    } else {
      return '#22c55e'
    }
  }
  public getImg() {
    return this.label != 'M' 
      ? './assets/icons/hombre.png'
      : './assets/icons/mujer.png';
  }

  public getTextDescribe() {
    if (this.label == 'M') {
      return 'Maternal'
    } else {
      return 'Parental'
    }
  }

  getLabelGender(gender: number) {
    if (gender == 0) {
      return 'Indefinido';

    } else if (gender == 1) {
      return 'Masculino';
    }
    return 'Femenino';
  }

}
