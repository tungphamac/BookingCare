import { DoctorSearch } from './doctor-search.model';
import { ClinicSearch } from './clinic-search.model';
import { SpecializationSearch } from './specialization-search.model';
import { PatientDetail } from './patient-search.model';
export interface SearchResult {
  message: string;
  doctors: DoctorSearch[];
  clinics: ClinicSearch[];
  specializations: SpecializationSearch[];
  patients: PatientDetail[];
}