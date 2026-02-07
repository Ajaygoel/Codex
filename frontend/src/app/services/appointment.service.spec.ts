import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AppointmentService } from './appointment.service';
import { AppointmentRequest } from '@app/models/appointment-request.model';
import { AppointmentSummary } from '@app/models/appointment-summary.model';
import { environment } from '../../environments/environment';

describe('AppointmentService', () => {
  let service: AppointmentService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(AppointmentService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('fetches upcoming appointments from the API', () => {
    const response: AppointmentSummary[] = [
      {
        appointmentId: 'apt-1',
        patientId: 'patient-1',
        patientName: 'Alex Carter',
        scheduledFor: '2024-06-01T09:00:00Z',
        reason: 'Checkup',
        status: 'Scheduled',
        initialDiagnosisSummary: null
      }
    ];

    service.getUpcoming().subscribe((appointments) => {
      expect(appointments).toEqual(response);
    });

    const request = httpMock.expectOne(`${environment.apiBaseUrl}/api/appointments`);
    expect(request.request.method).toBe('GET');
    request.flush(response);
  });

  it('posts appointment requests to the API', () => {
    const payload: AppointmentRequest = {
      patient: {
        firstName: 'Jordan',
        lastName: 'Lee',
        dateOfBirth: '1988-05-06'
      },
      scheduledFor: '2024-06-01T09:00:00Z',
      reason: 'Consultation',
      initialDiagnosis: null
    };

    const response: AppointmentSummary = {
      appointmentId: 'apt-2',
      patientId: 'patient-2',
      patientName: 'Jordan Lee',
      scheduledFor: '2024-06-01T09:00:00Z',
      reason: 'Consultation',
      status: 'Requested',
      initialDiagnosisSummary: null
    };

    service.requestAppointment(payload).subscribe((appointment) => {
      expect(appointment).toEqual(response);
    });

    const request = httpMock.expectOne(`${environment.apiBaseUrl}/api/appointments`);
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual(payload);
    request.flush(response);
  });
});
