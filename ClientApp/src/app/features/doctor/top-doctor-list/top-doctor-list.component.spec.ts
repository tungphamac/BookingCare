import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopDoctorListComponent } from './top-doctor-list.component';

describe('TopDoctorListComponent', () => {
  let component: TopDoctorListComponent;
  let fixture: ComponentFixture<TopDoctorListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopDoctorListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopDoctorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
