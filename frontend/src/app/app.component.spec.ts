import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AppComponent } from './app.component';
import { AppointmentService } from './services/appointment.service';
import { TenantService } from './services/tenant.service';
import { TenantContextService } from './services/tenant-context.service';
import { UserContextService } from './services/user-context.service';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;
  let appointmentService: jasmine.SpyObj<AppointmentService>;
  let tenantService: jasmine.SpyObj<TenantService>;
  let tenantContext: jasmine.SpyObj<TenantContextService>;
  let userContext: jasmine.SpyObj<UserContextService>;

  beforeEach(async () => {
    appointmentService = jasmine.createSpyObj('AppointmentService', ['getUpcoming', 'requestAppointment']);
    tenantService = jasmine.createSpyObj('TenantService', ['getTenants']);
    tenantContext = jasmine.createSpyObj('TenantContextService', ['getTenantId', 'setTenantId']);
    userContext = jasmine.createSpyObj('UserContextService', ['getUserId', 'getUserName', 'getUserRoles', 'setUserContext']);

    appointmentService.getUpcoming.and.returnValue(of([]));
    tenantService.getTenants.and.returnValue(of([]));
    tenantContext.getTenantId.and.returnValue('north-clinic');
    userContext.getUserId.and.returnValue('demo.user');
    userContext.getUserName.and.returnValue('Demo User');
    userContext.getUserRoles.and.returnValue('doctor');

    await TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [
        { provide: AppointmentService, useValue: appointmentService },
        { provide: TenantService, useValue: tenantService },
        { provide: TenantContextService, useValue: tenantContext },
        { provide: UserContextService, useValue: userContext }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('loads upcoming appointments and tenants on init', () => {
    expect(appointmentService.getUpcoming).toHaveBeenCalled();
    expect(tenantService.getTenants).toHaveBeenCalled();
  });

  it('prevents submission when the form is invalid', () => {
    component.submit();

    expect(appointmentService.requestAppointment).not.toHaveBeenCalled();
    expect(component.form.touched).toBeTrue();
  });

  it('submits a valid appointment request and updates the list', () => {
    const appointment = {
      appointmentId: 'apt-10',
      patientId: 'patient-10',
      patientName: 'Sky Rose',
      scheduledFor: '2024-07-01T08:00:00Z',
      reason: 'Consultation',
      status: 'Scheduled',
      initialDiagnosisSummary: 'Migraines'
    };

    appointmentService.requestAppointment.and.returnValue(of(appointment));

    component.form.setValue({
      firstName: 'Sky',
      lastName: 'Rose',
      dateOfBirth: '1990-02-01',
      scheduledFor: '2024-07-01T08:00:00Z',
      reason: 'Consultation',
      diagnosisSummary: 'Migraines',
      diagnosisNotes: 'Recurring'
    });

    component.submit();

    expect(appointmentService.requestAppointment).toHaveBeenCalledWith({
      patient: {
        firstName: 'Sky',
        lastName: 'Rose',
        dateOfBirth: '1990-02-01'
      },
      scheduledFor: '2024-07-01T08:00:00Z',
      reason: 'Consultation',
      initialDiagnosis: {
        summary: 'Migraines',
        notes: 'Recurring'
      }
    });
    expect(component.upcomingAppointments[0]).toEqual(appointment);
    expect(component.isSubmitting).toBeFalse();
  });

  it('updates the tenant context and refreshes appointments', () => {
    appointmentService.getUpcoming.calls.reset();

    component.tenantForm.setValue({
      tenantId: 'east-clinic'
    });

    component.updateTenant();

    expect(tenantContext.setTenantId).toHaveBeenCalledWith('east-clinic');
    expect(appointmentService.getUpcoming).toHaveBeenCalled();
  });

  it('updates the user context from the form', () => {
    component.userForm.setValue({
      userId: 'care.user',
      userName: 'Care User',
      roles: 'admin'
    });

    component.updateUserContext();

    expect(userContext.setUserContext).toHaveBeenCalledWith('care.user', 'Care User', 'admin');
  });
});
