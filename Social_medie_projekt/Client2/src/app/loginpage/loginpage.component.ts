import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  FormsModule,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Login } from '../_models/login';
import { Role } from '../_models/role';
import { AuthService } from '../_services/auth.service';
// import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-loginpage',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: 'loginpage.component.html',
  styleUrls: ['loginpage.component.css'],
})
export class LoginpageComponent {
  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute // private AppComponent: AppComponent
  ) {}

  message = '';
  userForm: FormGroup = this.resetForm();
  userlogin: Login = { loginId: 0, email: '', password: '' };

  resetForm(): FormGroup {
    return new FormGroup({
      Email: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
      ]),
      Password: new FormControl(null, Validators.required),
    });
  }

  login(): void {
    this.auth
      .login(this.userForm.value.Email, this.userForm.value.Password)
      .subscribe({
        next: () => {
          let returnUrl =
            this.route.snapshot.queryParams['returnUrl'] || '/main';
          this.router.navigate([returnUrl]);

          // opretter en talkservice til chat når user logger ind
          // this.talkService.createCurrentSession();

          // ændrer headeren
          // this.AppComponent.validateHeader();
        },

        error: (err) => {
          // change inputfields to red border
          document.getElementById('Email')!.style.borderColor = 'red';
          document.getElementById('Password')!.style.borderColor = 'red';

          if (err.error.status == 400) {
            this.message = 'Indtast brugernavn og kodeord';
            console.log(this.message);
            console.log(err.message);
          }
          if (err.error.status == 401) {
            this.message = 'Forkert brugernavn eller kodeord';
            console.log(this.message);
          }
          if (err.error.status == 500) {
            this.message = 'Fejl ved forbindelse til server';
            document.getElementById('Email')!.style.borderColor = 'yellow';
            document.getElementById('Password')!.style.borderColor = 'yellow';
            console.log(this.message);
            console.log(err);
          } else {
            this.message = err.message;
            console.log(this.message);
            console.log(err);
          }
        },
      });
  }
}
