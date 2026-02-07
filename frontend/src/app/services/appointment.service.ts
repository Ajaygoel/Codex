import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppointmentRequest } from '@app/models/appointment-request.model';
import { AppointmentSummary } from '@app/models/appointment-summary.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private readonly baseUrl = `${environment.apiBaseUrl}/api/appointments`;

  constructor(private readonly http: HttpClient) {}

  getUpcoming(): Observable<AppointmentSummary[]> {
    return this.http.get<AppointmentSummary[]>(this.baseUrl);
  }

  requestAppointment(request: AppointmentRequest): Observable<AppointmentSummary> {
    return this.http.post<AppointmentSummary>(this.baseUrl, request);
  }
}
