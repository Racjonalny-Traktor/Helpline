# -*- coding: utf-8 -*-
import scrapy


class WykopSpiderSpider(scrapy.Spider):
    name = 'wykop_spider'
    allowed_domains = ['wykop.pl']
    start_urls = ['http://wykop.pl/']

    def parse(self, response):
        pass
