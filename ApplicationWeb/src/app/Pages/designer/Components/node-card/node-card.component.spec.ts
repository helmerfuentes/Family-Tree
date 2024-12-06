/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NodeCardComponent } from './node-card.component';

describe('NodeCardComponent', () => {
  let component: NodeCardComponent;
  let fixture: ComponentFixture<NodeCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NodeCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NodeCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
