import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAppointmentDetailComponent } from './view-appointment-detail.component';

describe('ViewAppointmentDetailComponent', () => {
  let component: ViewAppointmentDetailComponent;
  let fixture: ComponentFixture<ViewAppointmentDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewAppointmentDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewAppointmentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
