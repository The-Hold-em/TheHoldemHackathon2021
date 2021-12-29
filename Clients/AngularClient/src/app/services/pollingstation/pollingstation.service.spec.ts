import { TestBed } from '@angular/core/testing';

import { PollingstationService } from './pollingstation.service';

describe('PollingstationService', () => {
  let service: PollingstationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PollingstationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
