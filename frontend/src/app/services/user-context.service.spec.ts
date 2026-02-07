import { TestBed } from '@angular/core/testing';
import { UserContextService } from './user-context.service';

describe('UserContextService', () => {
  let service: UserContextService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserContextService);
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('userRoles');
  });

  it('returns default values when no user context is stored', () => {
    expect(service.getUserId()).toBe('demo.user');
    expect(service.getUserName()).toBe('Demo User');
    expect(service.getUserRoles()).toBe('doctor');
  });

  it('stores and retrieves user context values', () => {
    service.setUserContext('care.user', 'Care User', 'admin');

    expect(service.getUserId()).toBe('care.user');
    expect(service.getUserName()).toBe('Care User');
    expect(service.getUserRoles()).toBe('admin');
  });
});
