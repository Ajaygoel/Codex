import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class TenantContextService {
  private readonly storageKey = 'tenantId';

  getTenantId(): string {
    return localStorage.getItem(this.storageKey) ?? 'north-clinic';
  }

  setTenantId(tenantId: string): void {
    localStorage.setItem(this.storageKey, tenantId);
  }
}
