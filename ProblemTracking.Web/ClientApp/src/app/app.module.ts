import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { API_BASE_URL } from './shared/services/generated/api.client.generated';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
//import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { StepChangeComponent } from './step-change/step-change.component';
import { ViewProblemComponent } from './view-problem/view-problem.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { AdminGuard } from './shared/guards/admin.guard';
import { HttpInterceptorService } from './shared/services/http-interceptor.service';
import { ErrorInterceptorService } from './shared/services/error-interceptor.service';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AddProblemComponent } from './add-problem/add-problem.component'; 
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    //HomeComponent,
    //CounterComponent,
    LoginComponent,
    UserComponent, ViewProblemComponent,
    FetchDataComponent,
    StepChangeComponent,
    AddProblemComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, BrowserAnimationsModule,
      FormsModule, ReactiveFormsModule, MatDialogModule, MatCheckboxModule, MatInputModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
      { path: 'admin', component: FetchDataComponent, canActivate: [AdminGuard] },
    ])
  ],
  entryComponents: [StepChangeComponent, AddProblemComponent],
  providers: [{provide: API_BASE_URL, useValue: (window.location.origin).replace(/\/+$/, '')
    //provide: API_BASE_URL, useValue: ("http://localhost:5201")
  },
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
