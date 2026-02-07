import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TenantService } from './tenant.service';
import { Tenant } from '../models/tenant.model';
import { environment } from '../../environments/environment';

describe('TenantService', () => {
  let service: TenantService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(TenantService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('fetches tenant catalog from the admin API', () => {
    const response: Tenant[] = [
      {
        id: 'north-clinic',
        name: 'North Clinic',
        region: 'Midwest'
      }
    ];

    service.getTenants().subscribe((tenants) => {
      expect(tenants).toEqual(response);
    });

    const request = httpMock.expectOne(`${environment.apiBaseUrl}/api/admin/tenants`);
    expect(request.request.method).toBe('GET');
    request.flush(response);
  });
});
