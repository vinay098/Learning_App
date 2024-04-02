import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillModuleComponent } from './skill-module.component';

describe('SkillModuleComponent', () => {
  let component: SkillModuleComponent;
  let fixture: ComponentFixture<SkillModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SkillModuleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SkillModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
