import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthApiService } from 'src/app/services/api/auth-api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {

  public loginForm!: FormGroup;


  constructor(private authService: AuthApiService,
    private router: Router,
     private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onLogin():void {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

    this.authService.login(username, password).subscribe(
      response => {
        alert('Вход выполнен успешно');
        this.router.navigate(['/shortUrl']);

      },
      error => {
        alert('Ошибка входа:');
      }
    );
  }
}
}

