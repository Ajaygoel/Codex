import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DoctorDashboardComponent } from './doctor-dashboard.component';

describe('DoctorDashboardComponent', () => {
  let fixture: ComponentFixture<DoctorDashboardComponent>;
  let component: DoctorDashboardComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorDashboardComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(DoctorDashboardComponent);
    component = fixture.componentInstance;
  });

  it('shows an empty state when there are no appointments', () => {
    component.appointments = [];
    fixture.detectChanges();

    const emptyState = fixture.debugElement.query(By.css('.empty'));
    expect(emptyState.nativeElement.textContent).toContain('No appointments scheduled');
  });

  it('renders appointment details when provided', () => {
    component.appointments = [
      {
        appointmentId: 'apt-1',
        patientId: 'patient-1',
        patientName: 'Alex Carter',
        scheduledFor: '2024-06-01T09:00:00Z',
        reason: 'Follow-up',
        status: 'Scheduled',
        initialDiagnosisSummary: 'Hypertension'
      }
    ];
    fixture.detectChanges();

    const rows = fixture.debugElement.queryAll(By.css('li'));
    expect(rows.length).toBe(1);
    expect(rows[0].nativeElement.textContent).toContain('Alex Carter');
    expect(rows[0].nativeElement.textContent).toContain('Hypertension');
  });
});
