import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { UserViewModel } from '../shared/services/generated/api.client.generated';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading = false;
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    this.authService.userData.subscribe(u => {
      this.checkToredirect(u);
    });
  }

  get loginFormControl() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.login(this.loginForm.value)
      .subscribe(
        (x) => {
          //this.checkToredirect(this.authService.userData.value);
        },
        (e) => {
          this.loading = false;
          this.loginForm.reset();
          this.loginForm.setErrors({
            invalidLogin: true
          });
        });

  }
  checkToredirect(user: UserViewModel) {
    if (user && user.role == "Admin")
      this.router.navigate(['/admin']);
    else if (user && user.role == "User")
      this.router.navigate(['/user']);
    else { this.router.navigate(['']); }
  }
}
