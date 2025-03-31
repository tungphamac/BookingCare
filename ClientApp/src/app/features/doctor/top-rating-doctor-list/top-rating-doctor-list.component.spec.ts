import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopRatingDoctorListComponent } from './top-rating-doctor-list.component';

describe('TopRatingDoctorListComponent', () => {
  let component: TopRatingDoctorListComponent;
  let fixture: ComponentFixture<TopRatingDoctorListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopRatingDoctorListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopRatingDoctorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
