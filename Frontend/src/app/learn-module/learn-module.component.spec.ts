import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnModuleComponent } from './learn-module.component';

describe('LearnModuleComponent', () => {
  let component: LearnModuleComponent;
  let fixture: ComponentFixture<LearnModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LearnModuleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LearnModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
