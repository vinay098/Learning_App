import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacultyDataComponent } from './faculty-data.component';

describe('FacultyDataComponent', () => {
  let component: FacultyDataComponent;
  let fixture: ComponentFixture<FacultyDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FacultyDataComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FacultyDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
