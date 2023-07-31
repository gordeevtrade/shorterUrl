import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthApiService } from 'src/app/services/api/auth-api.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  registrationForm!: FormGroup;
  
  constructor(private formBuilder: FormBuilder,
    private authService: AuthApiService,
    private router: Router,

    ) { }

  ngOnInit() {
    this.registrationForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

   public onRegister():void {
      if (this.registrationForm.valid) {
        const { username, password } = this.registrationForm.value;
      this.authService.register(username, password).subscribe(
        response => {
          alert('Регистрация прошла успешно.');
          this.router.navigate(['/login']);
        },
        error => {
          alert('Ошибка при регистрации: Используйте  похожий шаблон при регистрации Andrei2023!');
        }
      );
    }
  }



  }


