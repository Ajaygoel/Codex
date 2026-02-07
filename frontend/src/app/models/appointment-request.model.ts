export interface PatientInput {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
}

export interface DiagnosisInput {
  summary: string;
  notes: string;
}

export interface AppointmentRequest {
  patient: PatientInput;
  scheduledFor: string;
  reason: string;
  initialDiagnosis?: DiagnosisInput | null;
}
