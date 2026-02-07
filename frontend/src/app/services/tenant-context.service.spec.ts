import { TestBed } from '@angular/core/testing';
import { TenantContextService } from './tenant-context.service';

describe('TenantContextService', () => {
  let service: TenantContextService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenantContextService);
    localStorage.removeItem('tenantId');
  });

  it('returns the default tenant when none is stored', () => {
    expect(service.getTenantId()).toBe('north-clinic');
  });

  it('stores and retrieves the tenant id', () => {
    service.setTenantId('east-clinic');

    expect(service.getTenantId()).toBe('east-clinic');
  });
});
