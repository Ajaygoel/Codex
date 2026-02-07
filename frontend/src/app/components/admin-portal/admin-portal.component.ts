import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Tenant } from '../../models/tenant.model';

@Component({
  selector: 'app-admin-portal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-portal.component.html',
  styleUrl: './admin-portal.component.css'
})
export class AdminPortalComponent {
  @Input({ required: true }) tenants: Tenant[] = [];
}
