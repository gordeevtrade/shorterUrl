import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { FullUrl } from 'src/app/models/FullUrl';
import { UrlShortService } from 'src/app/services/api/url-short.service';

@Component({
  selector: 'app-addurldialog',
  templateUrl: './addurldialog.component.html',
  styleUrls: ['./addurldialog.component.css']
})
export class AddurldialogComponent {

  form = this.fb.group({
    url: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, 
    private dialogRef: MatDialogRef<AddurldialogComponent>,
    private urlService: UrlShortService
    ) 
    {}


    public addUrl(): void {
      if (this.form.valid) {
        const url = this.form.value.url;
        if (url) {
          const fullUrl: FullUrl = { originalUrl: url };
          this.urlService.createUrl(fullUrl).subscribe(
            response => {
              this.dialogRef.close(true);
            },
            error => {
              alert('Ошибка при добавлении URL');
            }
          );
        } else {
          alert('URL не может быть пустым');
        }
      }
    }
    
  
  cancel(): void {
    this.dialogRef.close();

  }
}
