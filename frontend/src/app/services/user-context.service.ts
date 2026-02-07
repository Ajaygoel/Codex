import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserContextService {
  private readonly userIdKey = 'userId';
  private readonly userNameKey = 'userName';
  private readonly rolesKey = 'userRoles';

  getUserId(): string {
    return localStorage.getItem(this.userIdKey) ?? 'demo.user';
  }

  getUserName(): string {
    return localStorage.getItem(this.userNameKey) ?? 'Demo User';
  }

  getUserRoles(): string {
    return localStorage.getItem(this.rolesKey) ?? 'doctor';
  }

  setUserContext(userId: string, userName: string, roles: string): void {
    localStorage.setItem(this.userIdKey, userId);
    localStorage.setItem(this.userNameKey, userName);
    localStorage.setItem(this.rolesKey, roles);
  }
}
