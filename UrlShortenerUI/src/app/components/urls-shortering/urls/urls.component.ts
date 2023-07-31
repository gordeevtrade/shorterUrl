import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FullUrl } from 'src/app/models/FullUrl';
import { ShortUrl } from 'src/app/models/ShortUrl';
import { UrlShortService } from 'src/app/services/api/url-short.service';
import { AddurldialogComponent } from '../../dialog/addurldialog/addurldialog.component';
import { AuthApiService } from 'src/app/services/api/auth-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-urls',
  templateUrl: './urls.component.html',
  styleUrls: ['./urls.component.css']
})
export class UrlsComponent {
 public shortUrls?: ShortUrl[];

  constructor(private urlService: UrlShortService,
     private dialog: MatDialog,
     private auathService:AuthApiService,
     private router: Router,


     ) { 

  }


  ngOnInit() {
    this.loadUrls();

  }

  public deleteUrl(id: number): void {
    this.urlService.deleteUrl(id).subscribe(
      response => {
        alert('URL deleted successfully');
        this.loadUrls(); 

      },
      error => {
        alert('Error deleting URL');
      }
    );
  }

  public navigateToShortUrl(shortCode: string) {
    this.urlService.redirectToUrl(shortCode).subscribe(
      (response: FullUrl) => {
      const newWindow = window.open(response.originalUrl, '_blank');
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  public navigateToOriginaltUrl (originalUrl : string){
    const newWindow = window.open(originalUrl, '_blank');

  }
  
  logout() {
    this.auathService.logout();
    this.router.navigate(['/login']);
  }




  public openAddUrlDialog(): void {
    const dialogRef = this.dialog.open(AddurldialogComponent);

    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
        this.loadUrls();
        }
      }
    );
  }


  private loadUrls(): void {
    this.urlService.getUrls().subscribe(
      urls => {
        this.shortUrls = urls;
      },
      error => {
        console.error(error);
      }
    );
  }



}
