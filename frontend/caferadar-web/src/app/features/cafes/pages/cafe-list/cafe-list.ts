import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CafeService } from '../../services/cafe.services';
import { CafeStatus } from '../../models/cafe-status';
import { EspService } from '../../services/esp.service';

@Component({
  selector: 'app-cafe-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cafe-list.html',
  styleUrl: './cafe-list.scss'
})
export class CafeList implements OnInit, OnDestroy {
  private cafeService = inject(CafeService);
   private espService = inject(EspService);  

  cafes: CafeStatus[] = [];
  loading = false;


  private refreshIntervalId: any;

  selectedFilter: string = 'reset';  
  statusMessage: string = '';   

  ngOnInit(): void {
    this.loadData();

    this.refreshIntervalId = setInterval(() => {
      this.loadData();
    }, 2000);
  }

  ngOnDestroy(): void {
    if (this.refreshIntervalId) {
      clearInterval(this.refreshIntervalId);
    }
  }

  loadData() {
    this.loading = true;
    this.cafeService.getStatus().subscribe({
      next: (data) => {
        this.cafes = data;
        this.loading = false;
        //console.log('STATUS DATA', data);
      },
      error: (err) => {
        console.error('Greška', err);
        this.loading = false;
      }
    });
  }

  setFilter(filter: string) {
    this.selectedFilter = filter;
  }

  get filteredCafes() {
    return this.cafes.filter(c => this.matchesFilter(c));
  }

  private matchesFilter(cafe: CafeStatus): boolean {
    const noise = (cafe.noiseLevel || '').toLowerCase();  
    const light = (cafe.lightLevel || '').toLowerCase();  

    switch (this.selectedFilter) {
      case 'study':
        return noise === 'quiet';

      case 'coffee':
        const noiseOk = noise === 'normal';
        const lightOk = light === 'bright' || light === 'normal';
        return noiseOk && lightOk;

      case 'night':
        const noiseNightOk = noise === 'loud';
        const lightNightOk = light === 'dark' || light === 'normal';
        return noiseNightOk && lightNightOk;

      case 'reset':
      default:
        return true;
    }
  }


  triggerDisplay() {
  this.statusMessage = 'Sending request to device...';

    this.espService.showOnDisplay().subscribe({
      next: () => {
        this.statusMessage = 'Displayed on device!';
      },
      error: (err) => {
        console.error('ESP error', err);
        this.statusMessage = 'ESP is not available.';
      }
    });
}
}
