import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientEditComponent } from './edit-patient.component';

describe('EditPatientComponent', () => {
  let component: PatientEditComponent;
  let fixture: ComponentFixture<PatientEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
