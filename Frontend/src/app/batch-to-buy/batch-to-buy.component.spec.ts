import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchToBuyComponent } from './batch-to-buy.component';

describe('BatchToBuyComponent', () => {
  let component: BatchToBuyComponent;
  let fixture: ComponentFixture<BatchToBuyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatchToBuyComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatchToBuyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
