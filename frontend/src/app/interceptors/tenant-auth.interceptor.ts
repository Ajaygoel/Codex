import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TenantContextService } from '../services/tenant-context.service';
import { UserContextService } from '../services/user-context.service';

export const tenantAuthInterceptor: HttpInterceptorFn = (req, next) => {
  const tenantContext = inject(TenantContextService);
  const userContext = inject(UserContextService);

  const tenantId = tenantContext.getTenantId();
  const userId = userContext.getUserId();
  const userName = userContext.getUserName();
  const userRoles = userContext.getUserRoles();

  const authReq = req.clone({
    setHeaders: {
      'X-Tenant-Id': tenantId,
      'X-User-Id': userId,
      'X-User-Name': userName,
      'X-User-Roles': userRoles
    }
  });

  return next(authReq);
};
