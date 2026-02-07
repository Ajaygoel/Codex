import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { AdminPortalComponent } from './admin-portal.component';

describe('AdminPortalComponent', () => {
  let fixture: ComponentFixture<AdminPortalComponent>;
  let component: AdminPortalComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPortalComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(AdminPortalComponent);
    component = fixture.componentInstance;
  });

  it('shows an empty state when no tenants are configured', () => {
    component.tenants = [];
    fixture.detectChanges();

    const emptyState = fixture.debugElement.query(By.css('.empty'));
    expect(emptyState.nativeElement.textContent).toContain('No tenants configured');
  });

  it('lists tenant details when tenants are provided', () => {
    component.tenants = [
      {
        id: 'north-clinic',
        name: 'North Clinic',
        region: 'Midwest'
      }
    ];
    fixture.detectChanges();

    const cards = fixture.debugElement.queryAll(By.css('article'));
    expect(cards.length).toBe(1);
    expect(cards[0].nativeElement.textContent).toContain('North Clinic');
    expect(cards[0].nativeElement.textContent).toContain('north-clinic');
  });
});
