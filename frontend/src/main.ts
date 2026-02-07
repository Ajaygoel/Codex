import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from './app/app.component';
import { tenantAuthInterceptor } from './app/interceptors/tenant-auth.interceptor';

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(withInterceptors([tenantAuthInterceptor]))]
}).catch((error) => console.error(error));
