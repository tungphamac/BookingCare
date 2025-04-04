import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageMedicalRecordComponent } from './manage-medical-record.component';

describe('ManageMedicalRecordComponent', () => {
  let component: ManageMedicalRecordComponent;
  let fixture: ComponentFixture<ManageMedicalRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageMedicalRecordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageMedicalRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
