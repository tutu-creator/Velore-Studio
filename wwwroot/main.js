document.addEventListener('DOMContentLoaded', () => {
  // ==========================================================================
  // 1. MOBILE NAVIGATION & HAMBURGER
  // ==========================================================================
  const menuToggle = document.querySelector('.menu-toggle');
  const mobileNav = document.querySelector('.mobile-nav');

  if (menuToggle && mobileNav) {
    menuToggle.addEventListener('click', () => {
      menuToggle.classList.toggle('open');
      mobileNav.classList.toggle('open');

      if (mobileNav.classList.contains('open')) {
        document.body.style.overflow = 'hidden';
      } else {
        document.body.style.overflow = 'auto';
      }
    });

    const mobileLinks = mobileNav.querySelectorAll('a');
    mobileLinks.forEach(link => {
      link.addEventListener('click', () => {
        menuToggle.classList.remove('open');
        mobileNav.classList.remove('open');
        document.body.style.overflow = 'auto';
      });
    });
  }

  // ==========================================================================
  // 2. SCROLL EFFECT ON HEADER (STICKY HEADER)
  // ==========================================================================
  const header = document.querySelector('header');

  if (header) {
    const handleScroll = () => {
      if (window.scrollY > 50) {
        header.classList.add('scrolled');
      } else {
        header.classList.remove('scrolled');
      }
    };

    handleScroll();
    window.addEventListener('scroll', handleScroll);
  }

  // ==========================================================================
  // 3. TESTIMONIALS SLIDER
  // ==========================================================================
  const sliderContainer = document.querySelector('.testimonials-slider');

  if (sliderContainer) {
    const slides = sliderContainer.querySelectorAll('.testimonial-slide');
    const prevBtn = document.querySelector('.slider-btn.prev');
    const nextBtn = document.querySelector('.slider-btn.next');
    let currentSlide = 0;
    let slideInterval;

    const showSlide = (index) => {
      slides.forEach(slide => {
        slide.classList.remove('active');
      });

      if (index >= slides.length) {
        currentSlide = 0;
      } else if (index < 0) {
        currentSlide = slides.length - 1;
      } else {
        currentSlide = index;
      }

      slides[currentSlide].classList.add('active');
    };

    const nextSlide = () => {
      showSlide(currentSlide + 1);
    };

    const prevSlide = () => {
      showSlide(currentSlide - 1);
    };

    const startAutoPlay = () => {
      slideInterval = setInterval(nextSlide, 6000);
    };

    const stopAutoPlay = () => {
      clearInterval(slideInterval);
    };

    if (nextBtn) {
      nextBtn.addEventListener('click', () => {
        stopAutoPlay();
        nextSlide();
        startAutoPlay();
      });
    }

    if (prevBtn) {
      prevBtn.addEventListener('click', () => {
        stopAutoPlay();
        prevSlide();
        startAutoPlay();
      });
    }

    showSlide(currentSlide);
    startAutoPlay();
  }

  // ==========================================================================
  // 4. PORTFOLIO FILTERING
  // ==========================================================================
  const filterContainer = document.querySelector('.portfolio-filters');
  const portfolioGrid = document.querySelector('.portfolio-grid');

  if (filterContainer && portfolioGrid) {
    const filterBtns = filterContainer.querySelectorAll('.filter-btn');
    const portfolioItems = portfolioGrid.querySelectorAll('.portfolio-item');

    filterBtns.forEach(btn => {
      btn.addEventListener('click', () => {
        filterBtns.forEach(b => b.classList.remove('active'));
        btn.classList.add('active');

        const filterValue = btn.getAttribute('data-filter');

        portfolioItems.forEach(item => {
          const itemCategory = item.getAttribute('data-category');

          if (filterValue === 'all' || itemCategory.includes(filterValue)) {
            item.style.display = 'block';
            setTimeout(() => {
              item.style.opacity = '1';
              item.style.transform = 'scale(1)';
            }, 50);
          } else {
            item.style.opacity = '0';
            item.style.transform = 'scale(0.95)';
            setTimeout(() => {
              item.style.display = 'none';
            }, 300);
          }
        });
      });
    });
  }

});
