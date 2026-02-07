import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { AppointmentSummary } from '../../models/appointment-summary.model';

@Component({
  selector: 'app-doctor-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './doctor-dashboard.component.html',
  styleUrl: './doctor-dashboard.component.css'
})
export class DoctorDashboardComponent {
  @Input({ required: true }) appointments: AppointmentSummary[] = [];
}
