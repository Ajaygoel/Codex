import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AppointmentService } from './services/appointment.service';
import { AppointmentRequest } from './models/appointment-request.model';
import { AppointmentSummary } from './models/appointment-summary.model';
import { Tenant } from './models/tenant.model';
import { TenantContextService } from './services/tenant-context.service';
import { TenantService } from './services/tenant.service';
import { UserContextService } from './services/user-context.service';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { AdminPortalComponent } from './components/admin-portal/admin-portal.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DoctorDashboardComponent,
    AdminPortalComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  form: FormGroup;
  upcomingAppointments: AppointmentSummary[] = [];
  tenants: Tenant[] = [];
  isSubmitting = false;
  errorMessage = '';
  tenantForm: FormGroup;
  userForm: FormGroup;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly appointmentService: AppointmentService,
    private readonly tenantService: TenantService,
    private readonly tenantContext: TenantContextService,
    private readonly userContext: UserContextService
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

    this.tenantForm = this.formBuilder.group({
      tenantId: [this.tenantContext.getTenantId(), Validators.required]
    });

    this.userForm = this.formBuilder.group({
      userId: [this.userContext.getUserId(), Validators.required],
      userName: [this.userContext.getUserName(), Validators.required],
      roles: [this.userContext.getUserRoles(), Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadUpcoming();
    this.loadTenants();
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

  private loadTenants(): void {
    this.tenantService.getTenants().subscribe({
      next: (tenants) => {
        this.tenants = tenants;
      },
      error: () => {
        this.errorMessage = 'Unable to load tenant catalog.';
      }
    });
  }

  updateTenant(): void {
    if (this.tenantForm.invalid) {
      this.tenantForm.markAllAsTouched();
      return;
    }

    this.tenantContext.setTenantId(this.tenantForm.value.tenantId);
    this.loadUpcoming();
  }

  updateUserContext(): void {
    if (this.userForm.invalid) {
      this.userForm.markAllAsTouched();
      return;
    }

    this.userContext.setUserContext(
      this.userForm.value.userId,
      this.userForm.value.userName,
      this.userForm.value.roles
    );
  }
}
