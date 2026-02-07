import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AppointmentService } from './services/appointment.service';
import { AppointmentRequest } from './models/appointment-request.model';
import { AppointmentSummary } from './models/appointment-summary.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  form: FormGroup;
  upcomingAppointments: AppointmentSummary[] = [];
  isSubmitting = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly appointmentService: AppointmentService
  ) {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      scheduledFor: ['', Validators.required],
      reason: ['', Validators.required],
      diagnosisSummary: [''],
      diagnosisNotes: ['']
    });
  }

  ngOnInit(): void {
    this.loadUpcoming();
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.errorMessage = '';
    this.isSubmitting = true;

    const request: AppointmentRequest = {
      patient: {
        firstName: this.form.value.firstName,
        lastName: this.form.value.lastName,
        dateOfBirth: this.form.value.dateOfBirth
      },
      scheduledFor: this.form.value.scheduledFor,
      reason: this.form.value.reason,
      initialDiagnosis: this.form.value.diagnosisSummary
        ? {
            summary: this.form.value.diagnosisSummary,
            notes: this.form.value.diagnosisNotes ?? ''
          }
        : null
    };

    this.appointmentService.requestAppointment(request).subscribe({
      next: (appointment) => {
        this.upcomingAppointments = [appointment, ...this.upcomingAppointments];
        this.form.reset();
        this.isSubmitting = false;
      },
      error: () => {
        this.errorMessage = 'Unable to submit the appointment request. Please try again.';
        this.isSubmitting = false;
      }
    });
  }

  private loadUpcoming(): void {
    this.appointmentService.getUpcoming().subscribe({
      next: (appointments) => {
        this.upcomingAppointments = appointments;
      },
      error: () => {
        this.errorMessage = 'Unable to load upcoming appointments.';
      }
    });
  }
}
