// Tab Navigation Functionality
document.addEventListener('DOMContentLoaded', function() {
    // First tab section
    const tabBtns = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');

    if (tabBtns.length > 0) {
        tabBtns.forEach(btn => {
            btn.addEventListener('click', function() {
                const targetTab = this.getAttribute('data-tab');

                // Remove active class from all buttons and contents
                tabBtns.forEach(b => b.classList.remove('active'));
                tabContents.forEach(c => c.classList.remove('active'));

                // Add active class to clicked button
                this.classList.add('active');

                // Show corresponding content
                const targetContent = document.getElementById(`tab-${targetTab}`);
                if (targetContent) {
                    targetContent.classList.add('active');
                }
            });
        });
    }

    // Second tab section
    const tabBtns2 = document.querySelectorAll('.tab-btn-2');
    const tabContents2 = document.querySelectorAll('.tab-content-2');

    if (tabBtns2.length > 0) {
        tabBtns2.forEach(btn => {
            btn.addEventListener('click', function() {
                const targetTab = this.getAttribute('data-tab');

                // Remove active class from all buttons and contents
                tabBtns2.forEach(b => b.classList.remove('active'));
                tabContents2.forEach(c => c.classList.remove('active'));

                // Add active class to clicked button
                this.classList.add('active');

                // Show corresponding content
                const targetContent = document.getElementById(`tab-2-${targetTab}`);
                if (targetContent) {
                    targetContent.classList.add('active');
                }
            });
        });
    }
});

// Product Slider Functionality
document.addEventListener('DOMContentLoaded', function() {
    const sliderWrapper = document.querySelector('.product-slider');
    const prevBtn = document.querySelector('.slider-prev');
    const nextBtn = document.querySelector('.slider-next');

    if (sliderWrapper && prevBtn && nextBtn) {
        const sliderItems = document.querySelectorAll('.slider-product-item');
        const itemWidth = sliderItems[0].offsetWidth;
        const gap = 30; // gap between items
        const itemsVisible = 4; // number of items visible at once
        let currentIndex = 0;
        const maxIndex = Math.max(0, sliderItems.length - itemsVisible);

        // Update slider position
        function updateSlider() {
            const offset = currentIndex * (itemWidth + gap);
            sliderWrapper.style.transform = `translateX(-${offset}px)`;

            // Disable/enable buttons based on position
            prevBtn.disabled = currentIndex === 0;
            nextBtn.disabled = currentIndex >= maxIndex;

            // Add visual feedback for disabled state
            if (currentIndex === 0) {
                prevBtn.style.opacity = '0.5';
                prevBtn.style.cursor = 'not-allowed';
            } else {
                prevBtn.style.opacity = '1';
                prevBtn.style.cursor = 'pointer';
            }

            if (currentIndex >= maxIndex) {
                nextBtn.style.opacity = '0.5';
                nextBtn.style.cursor = 'not-allowed';
            } else {
                nextBtn.style.opacity = '1';
                nextBtn.style.cursor = 'pointer';
            }
        }

        // Previous button click
        prevBtn.addEventListener('click', function() {
            if (currentIndex > 0) {
                currentIndex--;
                updateSlider();
            }
        });

        // Next button click
        nextBtn.addEventListener('click', function() {
            if (currentIndex < maxIndex) {
                currentIndex++;
                updateSlider();
            }
        });

        // Initialize slider
        updateSlider();

        // Handle window resize
        let resizeTimer;
        window.addEventListener('resize', function() {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function() {
                currentIndex = 0;
                updateSlider();
            }, 250);
        });
    }
});

