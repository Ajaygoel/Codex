export interface AppointmentSummary {
  appointmentId: string;
  patientId: string;
  patientName: string;
  scheduledFor: string;
  reason: string;
  status: string;
  initialDiagnosisSummary?: string | null;
}
