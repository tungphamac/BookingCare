import { TestBed } from '@angular/core/testing';

import { ListDoctorService } from './list-doctor.service';

describe('ListDoctorService', () => {
  let service: ListDoctorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListDoctorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
