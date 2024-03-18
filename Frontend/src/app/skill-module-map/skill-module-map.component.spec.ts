import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillModuleMapComponent } from './skill-module-map.component';

describe('SkillModuleMapComponent', () => {
  let component: SkillModuleMapComponent;
  let fixture: ComponentFixture<SkillModuleMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SkillModuleMapComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SkillModuleMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
