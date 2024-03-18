import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchModuleMapComponent } from './batch-module-map.component';

describe('BatchModuleMapComponent', () => {
  let component: BatchModuleMapComponent;
  let fixture: ComponentFixture<BatchModuleMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatchModuleMapComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatchModuleMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
